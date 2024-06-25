using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.context;
using RevenueRecognitionSystem.DTOs;
using RevenueRecognitionSystem.Mappers;
using RevenueRecognitionSystem.models;
using RevenueRecognitionSystem.services;

namespace RevenueRecognitionSystem.Tests;

public class ClientsServiceTests
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    private readonly ClientService _clientService;

    public ClientsServiceTests()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        _mapper = mappingConfig.CreateMapper();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _clientService = new ClientService(_context, _mapper);
    }

    [Fact]
    public async Task AddIndividualClientAsync_ShouldAddClient()
    {
        var clientDto = new IndividualClientDto
        {
            FirstName = "Janusz",
            LastName = "Kowalski",
            Address = "Miła 12",
            Email = "janusz@gmail.com",
            PhoneNumber = "123456789",
            PESEL = "123123123"
        };

        await _clientService.AddClientAsync(clientDto);

        var client = await _context.IndividualClients.FirstOrDefaultAsync();
        Assert.NotNull(client);
        Assert.Equal("Janusz", client.FirstName);
    }

    [Fact]
    public async Task UpdateIndividualClientAsync_ShouldUpdateClient()
    {
        var client = new IndividualClient
        {
            FirstName = "Janusz",
            LastName = "Kowalski",
            Address = "Miła 12",
            Email = "janusz@gmail.com",
            PhoneNumber = "123456789",
            PESEL = "123123123"
        };
        _context.IndividualClients.Add(client);
        await _context.SaveChangesAsync();

        var clientDto = new IndividualClientDto
        {
            Id = client.Id,
            FirstName = "Monika",
            LastName = "Nowak",
            Address = "Niemiła 123",
            Email = "monika@gmail.com",
            PhoneNumber = "123456789",
            PESEL = "123123123"
        };

        await _clientService.UpdateClientAsync(clientDto);

        var updatedClient = await _context.IndividualClients.FindAsync(client.Id);
        Assert.Equal("Monika", updatedClient.FirstName);
        Assert.Equal("monika@gmail.com", updatedClient.Email);
    }

    [Fact]
    public async Task RemoveIndividualClientAsync_ShouldSoftDeleteClient()
    {
        var client = new IndividualClient
        {
            FirstName = "Janusz",
            LastName = "Kowalski",
            Address = "Miła 12",
            Email = "janusz@gmail.com",
            PhoneNumber = "123456789",
            PESEL = "123123123",
            IsDeleted = false
        };
        _context.IndividualClients.Add(client);
        await _context.SaveChangesAsync();

        await _clientService.RemoveClientAsync(client.Id);

        var deletedClient = await _context.IndividualClients.FindAsync(client.Id);
        Assert.True(deletedClient.IsDeleted);
    }

    [Fact]
    public async Task RemoveCompanyClientAsync_ShouldThrowException()
    {
        var client = new CompanyClient
        {
            CompanyName = "E Corp",
            Address = "New York, NY, 10022-2050",
            Email = "contact@ecorp.com",
            PhoneNumber = "+1 0987654321",
            KRS = "9876543210"
        };
        _context.CompanyClients.Add(client);
        await _context.SaveChangesAsync();

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _clientService.RemoveClientAsync(client.Id));
    }
}
