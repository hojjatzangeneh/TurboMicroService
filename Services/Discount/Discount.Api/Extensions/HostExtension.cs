using Npgsql;

namespace Discount.Api.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("migrating posgtresql database");
                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSetting:ConnectionString"));
                    connection.Open();
                    using var command = new NpgsqlCommand { Connection = connection };
                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS public.coupon
(
    id integer NOT NULL,
    productname character varying(50) NOT NULL,
    description text ,
    amount integer,
    CONSTRAINT coupon_pkey PRIMARY KEY (id)
)";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO public.coupon(
	id, productname, description, amount)
	VALUES (1, 'Hojjat', '8', 8);";
                    command.ExecuteNonQuery();
                    command.CommandText = @"INSERT INTO public.coupon(
	id, productname, description, amount)
	VALUES (2, 'Zangeneh', 5, 5);";
                    command.ExecuteNonQuery();
                    logger.LogInformation("Migration has been copmpkated");
                }
                catch (Exception)
                {
                    logger.LogError("an errorhas been occured");
                    if (retryForAvailability < 50) { retryForAvailability++; Thread.Sleep(2000); MigrateDatabase<TContext>(host, retryForAvailability); }
                }
            }
            return host;
        }
    }
}
