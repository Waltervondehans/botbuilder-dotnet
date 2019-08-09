﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Schema;
using Moq;
using Xunit;

namespace Microsoft.Bot.Builder.Adapters.Twilio.Tests
{
    public class TwilioAdapterTests
    {
        [Fact]
        public void Constructor_Should_Fail_With_Null_Options()
        {
            Assert.Throws<ArgumentNullException>(() => { new TwilioAdapter(null, new Mock<ITwilioClient>().Object); });
        }

        [Fact]
        public void Constructor_Should_Fail_With_Null_TwilioNumber()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.AccountSid = "Test";

            Assert.Throws<Exception>(() => { new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object); });
        }

        [Fact]
        public void Constructor_Should_Fail_With_Null_AccountSid()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";

            Assert.Throws<Exception>(() => { new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object); });
        }

        [Fact]
        public void Constructor_Should_Fail_With_Null_AuthToken()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            Assert.Throws<Exception>(() => { new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object); });
        }

        [Fact]
        public void Constructor_WithArguments_Succeeds()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            Assert.NotNull(new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object));
        }

        [Fact]
        public async void SendActivitiesAsync_Should_Fail_With_ActivityType_Not_Message()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);

            var activity = new Activity()
            {
                Type = ActivityTypes.Event,
            };

            Activity[] activities = { activity };

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await twilioAdapter.SendActivitiesAsync(new TurnContext(twilioAdapter, activity), activities, default);
            });
        }

        [Fact]
        public async void ProcessAsync_Should_Fail_With_Null_HttpRequest()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);
            var httpResponse = new Mock<HttpResponse>();
            var bot = new Mock<IBot>();

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await twilioAdapter.ProcessAsync(null, httpResponse.Object, bot.Object, default(CancellationToken));
            });
        }

        [Fact]
        public async void ProcessAsync_Should_Fail_With_Null_HttpResponse()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);
            var httpRequest = new Mock<HttpRequest>();
            var bot = new Mock<IBot>();

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await twilioAdapter.ProcessAsync(httpRequest.Object, null, default(IBot), default(CancellationToken));
            });
        }

        [Fact]
        public async void ProcessAsync_Should_Fail_With_Null_Bot()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);
            var httpRequest = new Mock<HttpRequest>();
            var httpResponse = new Mock<HttpResponse>();

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await twilioAdapter.ProcessAsync(httpRequest.Object, httpResponse.Object, null, default(CancellationToken));
            });
        }

        [Fact(Skip = "Can't mock extension methods")]
        public async void ProcessAsync_Should_Succeed_With_HttpBody()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);
            var httpRequest = new Mock<HttpRequest>();
            var httpResponse = new Mock<HttpResponse>();
            httpResponse
                .Setup(e => e.WriteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await twilioAdapter.ProcessAsync(httpRequest.Object, httpResponse.Object, null, default(CancellationToken));
        }

        [Fact(Skip = "Can't mock extension methods")]
        public async void ProcessAsync_Should_Succeed_With_Null_HttpBody()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);
            var httpRequest = new Mock<HttpRequest>();
            var httpResponse = new Mock<HttpResponse>();
            httpResponse
                .Setup(e => e.WriteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await twilioAdapter.ProcessAsync(httpRequest.Object, httpResponse.Object, null, default(CancellationToken));
        }

        [Fact]
        public async void UpdateActivityAsync_Should_Throw_NotSupportedException()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);
            var activity = new Activity();
            var turnContext = new TurnContext(twilioAdapter, activity);

            await Assert.ThrowsAsync<NotSupportedException>(async () => { await twilioAdapter.UpdateActivityAsync(turnContext, activity, default); });
        }

        [Fact]
        public async void DeleteActivityAsync_Should_Throw_NotSupportedException()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);
            var activity = new Activity();
            var turnContext = new TurnContext(twilioAdapter, activity);
            var conversationReference = new ConversationReference();

            await Assert.ThrowsAsync<NotSupportedException>(async () => { await twilioAdapter.DeleteActivityAsync(turnContext, conversationReference, default); });
        }

        [Fact]
        public async void ContinueConversationAsync_Should_Fail_With_Null_ConversationReference()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);

            Task BotsLogic(ITurnContext turnContext, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }

            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await twilioAdapter.ContinueConversationAsync(null, BotsLogic, default); });
        }

        [Fact]
        public async void ContinueConversationAsync_Should_Fail_With_Null_Logic()
        {
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);
            var conversationReference = new ConversationReference();

            await Assert.ThrowsAsync<ArgumentNullException>(async () => { await twilioAdapter.ContinueConversationAsync(conversationReference, null, default); });
        }

        [Fact]
        public async void ContinueConversationAsync_Should_Succeed()
        {
            bool callbackInvoked = false;
            var options = new Mock<ITwilioAdapterOptions>();
            options.SetupAllProperties();
            options.Object.AuthToken = "Test";
            options.Object.TwilioNumber = "Test";
            options.Object.AccountSid = "Test";

            var twilioAdapter = new TwilioAdapter(options.Object, new Mock<ITwilioClient>().Object);
            var conversationReference = new ConversationReference();

            Task BotsLogic(ITurnContext turnContext, CancellationToken cancellationToken)
            {
                callbackInvoked = true;
                return Task.CompletedTask;
            }

            await twilioAdapter.ContinueConversationAsync(conversationReference, BotsLogic, default);
            Assert.True(callbackInvoked);
        }
    }
}
