using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartBearServer.Data;
using System.Text;
using SmartBearServer.Hubs;
using SmartBearServer.Infrastructure;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Repositories.Implementations;
using SmartBearServer.Services;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Services.Implementations;
using SmartBearServer.Services.Strategies;
using StackExchange.Redis;

namespace SmartBearServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // KILL ALL PROCESSES USING PORTS 7017 OR 7018 BEFORE STARTING
            PortGuard.CleanupPorts(7017, 7018);

            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(7017); // Allow external access for Mobile App & Bridge
            });
            builder.WebHost.UseStaticWebAssets();

            // Add Entity Framework Core DbContext
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Configure Redis
            var redisConfig = builder.Configuration.GetSection("Redis");
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConfig["Configuration"];
                options.InstanceName = redisConfig["InstanceName"];
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(redisConfig["Configuration"]!, true);
                configuration.AbortOnConnectFail = false;
                return ConnectionMultiplexer.Connect(configuration);
            });

            // Configure JWT Authentication
            var jwtConfig = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Key"] ?? "fallback_secret_key");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie("AdminCookies", options =>
            {
                options.Cookie.Name = "AdminCookies";
                options.LoginPath = "/admin/login";
                options.AccessDeniedPath = "/admin/login";
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["Issuer"],
                    ValidAudience = jwtConfig["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });

            // Configure Authorization Policies
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("1");
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddHttpClient<GeminiClient>();
            builder.Services.AddHttpClient<OpenAIClient>();

            builder.Services.AddScoped<IAIService, AIService>(); 
            builder.Services.AddHttpClient<OpenAIService>();

            // Register Infrastructure
            builder.Services.AddSingleton<IFileStorageService, FileStorageService>();
            builder.Services.AddSingleton<IStorageService, GcpStorageService>();

            // Register Strategy Factories
            builder.Services.AddSingleton<VoucherStrategyFactory>();
            builder.Services.AddSingleton<ModeInstructionStrategyFactory>();

            // Register Repositories
            builder.Services.AddScoped<IChatSessionRepository, ChatSessionRepository>();
            builder.Services.AddScoped<IChildProfileRepository, ChildProfileRepository>();
            builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
            builder.Services.AddScoped<ISongRepository, SongRepository>();
            builder.Services.AddScoped<IStoryRepository, StoryRepository>();
            builder.Services.AddScoped<ISmartAlarmRepository, SmartAlarmRepository>();
            builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBannedWordRepository, BannedWordRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
            builder.Services.AddScoped<BearCommandProcessor>();

            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ILearningRecommendationPromptBuilder, LearningRecommendationPromptBuilder>();
            builder.Services.AddScoped<IDeviceIdentityService, DeviceIdentityService>();
            builder.Services.AddScoped<IPromptBuilder, PromptBuilder>();
            builder.Services.AddScoped<IAIActionHandler, SmartBearServer.Services.Implementations.AiActions.PlaySongHandler>();
            builder.Services.AddScoped<IContentSafetyService, ContentSafetyService>();
            builder.Services.AddScoped<ISpeechService, SpeechService>();
            builder.Services.AddScoped<IUsageQuotaService, UsageQuotaService>();
            builder.Services.AddScoped<ISubscriptionLifecycleService, SubscriptionLifecycleService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IPayOSService, PayOSService>();
            builder.Services.AddScoped<IBannedWordService, BannedWordService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<GcsSyncService>();
            builder.Services.AddScoped<IPairingService, PairingService>();
            builder.Services.AddScoped<IDeviceService, DeviceService>();
            builder.Services.AddScoped<ISmartAlarmService, SmartAlarmService>();
            builder.Services.AddScoped<IVoiceService, VoiceService>();
            builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<IStoryPlaybackService, StoryPlaybackService>();
            builder.Services.AddScoped<ICacheService, RedisCacheService>();

            // TTS Services
            builder.Services.AddHttpClient<ElevenLabsService>();
            builder.Services.AddScoped<GoogleCloudTTSService>();
            
            // Register multiple TTS implementations
            builder.Services.AddScoped<ITTSService, GoogleCloudTTSService>();
            builder.Services.AddScoped<ITTSService, ElevenLabsService>();

            builder.Services.AddScoped<MusicService>();
            builder.Services.AddHostedService<SmartAlarmBackgroundService>();
            builder.Services.AddHostedService<QuotaResetBackgroundService>();
            builder.Services.AddHostedService<SessionCleanupWorker>();
            builder.Services.AddHostedService<ChatPersistenceWorker>();
            builder.Services.AddHostedService<OrderReconciliationWorker>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SmartBear API", Version = "v1" });
                
                
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,                 
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            builder.Services.AddSignalR(options => {
                options.EnableDetailedErrors = true;
                options.MaximumReceiveMessageSize = 1024 * 1024;
            });

            var app = builder.Build();

            // Startup Tasks: Sync Banned Words
            using (var scope = app.Services.CreateScope())
            {
                var bannedWordService = scope.ServiceProvider.GetRequiredService<IBannedWordService>();
                try
                {
                    await bannedWordService.SyncGlobalBannedWordsAsync();
                    Console.WriteLine("\x1b[32m[Startup] Banned words synchronized with database.\x1b[0m");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\x1b[31m[Startup] Error syncing banned words: {ex.Message}\x1b[0m");
                }
                
                // Clear stale profile configs on startup
                var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();
                await cache.RemoveByPatternAsync("bear:profile:config:*");
                Console.WriteLine("\x1b[32m[Startup] Bear profile config cache cleared.\x1b[0m");
            }
            
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHub<LLMHub>("/hubs/llm");

            app.Run();
        }
    }
}