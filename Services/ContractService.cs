using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.context;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.models;

namespace RevenueRecognitionSystem.services;

public class ContractService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ContractService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ContractDto> CreateContractAsync(int clientId, int softwareId, int supportYears)
    {
        var client = await _context.Clients.Include(c => c.Contracts).Include(c => c.Subscriptions).FirstOrDefaultAsync(c => c.Id == clientId);
        var software = await _context.Software.FindAsync(softwareId);

        if (client == null || software == null)
        {
            throw new ArgumentException("Invalid client or software ID.");
        }

        var highestDiscount = software.Discounts
            .Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
            .OrderByDescending(d => d.Percentage)
            .FirstOrDefault();

        decimal price = software.UpfrontCost;

        if (highestDiscount != null)
        {
            price -= price * (highestDiscount.Percentage / 100);
        }
        
        var previousClientDiscount = client.Contracts.Any() || client.Subscriptions.Any() ? 0.05m : 0m;
        price -= price * previousClientDiscount;
        
        price += supportYears * 1000;

        var contract = new Contract
        {
            ClientId = clientId,
            SoftwareId = softwareId,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(30),
            Price = price,
            SupportYears = supportYears,
            IsPaid = false,
            IsCancelled = false
        };

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        return _mapper.Map<ContractDto>(contract);
    }

    public async Task<PaymentDto> MakePaymentAsync(int contractId, decimal amount)
    {
        var contract = await _context.Contracts.FindAsync(contractId);
        if (contract == null || contract.IsCancelled || contract.EndDate < DateTime.Now)
        {
            throw new ArgumentException("Invalid or expired contract.");
        }

        var totalPaid = _context.Payments.Where(p => p.ContractId == contractId).Sum(p => p.Amount);
        if (totalPaid + amount > contract.Price)
        {
            throw new ArgumentException("Payment exceeds contract price.");
        }

        var payment = new Payment
        {
            ContractId = contractId,
            Amount = amount,
            PaymentDate = DateTime.Now
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        
        if (totalPaid + amount == contract.Price)
        {
            contract.IsPaid = true;
            await _context.SaveChangesAsync();
        }

        return _mapper.Map<PaymentDto>(payment);
    }
}