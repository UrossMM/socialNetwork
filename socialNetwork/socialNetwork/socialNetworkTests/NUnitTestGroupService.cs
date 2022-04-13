using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using socialNetwork;
using socialNetwork.Models;
using socialNetwork.Repositories;
using socialNetwork.Services;
using System;

namespace socialNetworkTests
{
    public class NUnitTestGroupService
    {
        IServiceProvider _serviceProvider;
        IServiceCollection _services;
        private  IGroupService _groupService;

        [OneTimeSetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
            _services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-WebApp1-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true"));
            _services.AddTransient<IGroupRepo, GroupRepo>();
            _services.AddTransient<IGroupService, GroupService>();
            _services.AddAutoMapper(typeof(Startup));
            _serviceProvider = _services.BuildServiceProvider(); 

            _groupService = (IGroupService)_serviceProvider.GetService(typeof(IGroupService));
        }

        [Test, Order(1)]
        public void Test1()
        {
            var result = _groupService.GetGroupById(3);
            Assert.That(result, Is.Not.Null);
        }

        [Test, Order(2)]
        public void TestGetAllGroups()
        {
            var result = _groupService.GetAllGroups();
            Assert.That(result.Count, Is.EqualTo(5));
        }

        [Test, Order(4)]
        public void TestCreateGroupValid()
        {
            //Da bi ova funkcija vracala success uvek kada se testira dodati novo ime za grupu
            Group g = new Group()
            { Name = "DigitalniMarketing" }; // U CreateGroup funkciji u GroupRepo se proverava da li samo jedna grupa ima to ime. 
            var result = _groupService.CreateGroup(g, "dfff0cc9-ef94-454c-835e-20cb9ab2f315"); //on je ovde upisuje u bazu prvi put kad testiramo, svaki naredni vraca null za istu grupu
            Assert.That(result, Is.Not.Null);
        }

        [Test, Order(3)]
        public void TestCreateGroupNotValid()
        {
            Group g = new Group()
            { Name = "Zivotinje" }; //ova grupa vec postoji u bazi tako da ce ovde da ispitujemo da li je null jer CreateGroup ako ne napravi grupu vraca null
            var result = _groupService.CreateGroup(g, "dfff0cc9-ef94-454c-835e-20cb9ab2f315"); 
            Assert.That(result, Is.Null);
        }

        [Test, Order(5)]
        public void TestAllAdmins()
        {
            var result = _groupService.AllAdmins();
            Assert.That(result.Count, Is.EqualTo(3)); //proverava broj razlicitih objekata admin koje referencira tabela grupa
        }
    }
}