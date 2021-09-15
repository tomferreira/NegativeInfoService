using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Logging;
using NegativeInfoService.Domain.Entities;
using NegativeInfoService.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NegativeInfoService.Infra
{
    public class AWSNotificationQueue : IBureauNotificationQueue
    {
        // Replace this id for a real topic ARN
        private static string TOPIC_ARN = "arn:aws:sns:eu-west-2:XXXXXXXXXXX:NegativationBureauTopic";

        private readonly IAmazonSimpleNotificationService _simpleNotificationService;
        private readonly ILogger<AWSNotificationQueue> _logger;

        public AWSNotificationQueue(
            IAmazonSimpleNotificationService simpleNotificationService, 
            ILogger<AWSNotificationQueue> logger)
        {
            _simpleNotificationService = simpleNotificationService;
            _logger = logger;
        }

        public Task NotifyInclusionAsync(Negativation negativation)
        {
            return NotifyAsync(AWSRequestMessage.ActionType.Inclusion, negativation);
        }

        public Task NotifyExclusionAsync(Negativation negativation)
        {
            return NotifyAsync(AWSRequestMessage.ActionType.Exclusion, negativation);
        }

        private async Task NotifyAsync(AWSRequestMessage.ActionType action, Negativation negativation)
        {
            var message = new AWSRequestMessage()
            {
                Action = action,
                ClientId = negativation.ClientId,
                LegalDocument = negativation.LegalDocument,
                BankTransitionID = negativation.BankTransitionId
            };

            var request = new PublishRequest
            {
                Message = JsonConvert.SerializeObject(message),
                TopicArn = TOPIC_ARN
            };

            try
            {
                var response = await _simpleNotificationService.PublishAsync(request);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Successfully sent SNS message '{response.MessageId}'");
                }
                else
                {
                    _logger.LogWarning(
                        $"Received a failure response '{response.HttpStatusCode}' when sending SNS message '{response.MessageId ?? "Missing ID"}'");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception was thrown when publish request to AWS SNS");
            }
        }
    }
}
