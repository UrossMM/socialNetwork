using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using socialNetwork;
using socialNetwork.Controllers;
using socialNetwork.Models;
using socialNetwork.Repositories;
using socialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialNetworkTests
{
    public class NUnitTestGroupController
    {
        private IServiceCollection _services;
        private IServiceProvider _serviceProvider;
        private GroupController _groupController;

        [OneTimeSetUp]
        public void Setup()
        {
            _services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            _services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-WebApp1-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true"));
            _services.AddTransient<IGroupRepo, GroupRepo>();
            _services.AddTransient<IGroupService, GroupService>();
            _services.AddAutoMapper(typeof(Startup));

            _serviceProvider = _services.BuildServiceProvider();

            _groupController = new GroupController((IGroupService)_serviceProvider.GetService(typeof(IGroupService)), (IMapper)_serviceProvider.GetService(typeof(IMapper)));
        }

        [Test]
        public void TestMyGroups()
        {
            var result = _groupController.MyGroups("dfff0cc9-ef94-454c-835e-20cb9ab2f315");
            Assert.That(result, Is.TypeOf<OkObjectResult>()); //ovaj test vraca listu objekata i zato je OkObjectResult
        }

        [Test]
        public void TestAddUserToGroup()
        {
            //a sada funkcija controllera AddUserToGroup vraca samo Ok, bez objekata pa pisemo sledece:
            var result = _groupController.AddUserToGroup("dfff0cc9-ef94-454c-835e-20cb9ab2f315", 3);
            Assert.That(result, Is.TypeOf<OkResult>());
        }
    }
}
