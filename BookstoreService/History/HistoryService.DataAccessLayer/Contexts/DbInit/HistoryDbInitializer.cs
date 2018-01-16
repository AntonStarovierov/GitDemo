namespace HistoryService.DataAccessLayer.Contexts.DbInit
{
	public class HistoryDbInitializer
	{
		public static void Initialize(HistoryDbContext context)
		{
			context.Database.EnsureCreated();
		}
	}
}