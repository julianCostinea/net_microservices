﻿using Azure.Messaging.ServiceBus;
using Mango.Services.EmailAPI.Models.Dto;
using Mango.Services.EmailAPI.Services;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.EmailAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string emailCartQueue;
        private readonly IConfiguration _configuration;
        private ServiceBusProcessor _emailCartProcessor;
        private readonly EmailService _emailService;

        public AzureServiceBusConsumer(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");

            emailCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");

            var client = new ServiceBusClient(serviceBusConnectionString);

            _emailCartProcessor = client.CreateProcessor(emailCartQueue);
        }

        public async Task Start()
        {
            _emailCartProcessor.ProcessMessageAsync += OnEmailCartRequestReceived;
            _emailCartProcessor.ProcessErrorAsync += ErrorHandler;
            await _emailCartProcessor.StartProcessingAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs args)
        {
            //this is where you will receive message
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            CartDto objMessage = JsonConvert.DeserializeObject<CartDto>(body);
            try
            {
                //TODO - try to log email
                await _emailService.EmailCartAndLog(objMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Stop()
        {
            await _emailCartProcessor.StopProcessingAsync();
            await _emailCartProcessor.DisposeAsync();
        }
    }
}
