using Makabaka.Messages;
using Makabaka.Models;
using Makabaka.Network;
using Makabaka.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Makabaka
{
	/// <summary>
	/// IServiceCollection 扩展
	/// </summary>
	public static class IServiceCollectionExtensions
	{
		/// <summary>
		/// 添加 Makabaka 服务
		/// </summary>
		public static IServiceCollection AddMakabaka(this IServiceCollection services)
		{
            services.AddSingleton<IBotContext, BotContext>();
            services.AddHostedService(provider =>
            {
                if (provider.GetRequiredService<IBotContext>() is not BotContext botContext)
                    throw new InvalidOperationException("BotContext is not set.");
                return botContext;
            });
			services.AddSingleton<JsonConverter<Message>, MessageJsonConverter>();
			services.AddSingleton<JsonConverter<SexType>, SexTypeJsonConverter>();
			services.AddSingleton<JsonConverter<DateTime>, TimestampDateTimeJsonConverter>();
			services.AddTransient<ForwardWebSocketContext>();
			services.AddTransient<ReverseWebSocketContext>();

			return services;
		}
	}
}
