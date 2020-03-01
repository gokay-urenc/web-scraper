using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IAdvertisementDal, EfAdvertisementDal>()
                .AddSingleton<IAdvertisementService, AdvertisementService>()
                .BuildServiceProvider();

            var advertisementService = serviceProvider.GetService<IAdvertisementService>();
            advertisementService.GetData();

            Console.ReadKey();
        }
    }
}