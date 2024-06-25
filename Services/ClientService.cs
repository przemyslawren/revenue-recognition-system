using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.context;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.models;

namespace RevenueRecognitionSystem.services;

public class ClientService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ClientService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddClientAsync<T>(T clientDto) where T : class
    {
        if (clientDto is IndividualClientDto individualDto)
        {
            var client = _mapper.Map<IndividualClient>(individualDto);
            _context.IndividualClients.Add(client);
        }
        else if (clientDto is CompanyClientDto companyDto)
        {
            var client = _mapper.Map<CompanyClient>(companyDto);
            _context.CompanyClients.Add(client);
        }
        await _context.SaveChangesAsync();
    }

    public async Task UpdateClientAsync<T>(T clientDto) where T : class
    {
        if (clientDto is IndividualClientDto individualDto)
        {
            var existingClient = await _context.IndividualClients.FindAsync(individualDto.Id);
            if (existingClient != null && existingClient.PESEL != individualDto.PESEL)
            {
                throw new InvalidOperationException("Cannot change PESEL number.");
            }
            _mapper.Map(individualDto, existingClient);
        }
        else if (clientDto is CompanyClientDto companyDto)
        {
            var existingClient = await _context.CompanyClients.FindAsync(companyDto.Id);
            if (existingClient != null && existingClient.KRS != companyDto.KRS)
            {
                throw new InvalidOperationException("Cannot change KRS number.");
            }
            _mapper.Map(companyDto, existingClient);
        }
        await _context.SaveChangesAsync();
    }

    public async Task RemoveClientAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client is IndividualClient individualClient)
        {
            individualClient.IsDeleted = true;
            _context.IndividualClients.Update(individualClient);
        }
        else
        {
            throw new InvalidOperationException("Cannot delete company clients.");
        }
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<IndividualClientDto>> GetAllIndividualClientsAsync()
    {
        var clients = await _context.IndividualClients.Where(c => !c.IsDeleted).ToListAsync();
        return _mapper.Map<IEnumerable<IndividualClientDto>>(clients);
    }

    public async Task<IEnumerable<CompanyClientDto>> GetAllCompanyClientsAsync()
    {
        var clients = await _context.CompanyClients.ToListAsync();
        return _mapper.Map<IEnumerable<CompanyClientDto>>(clients);
    }
}