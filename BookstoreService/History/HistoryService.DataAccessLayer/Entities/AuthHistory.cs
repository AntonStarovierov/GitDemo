namespace HistoryService.DataAccessLayer.Entities
{
	public class AuthHistory
	{
		public int Id { get; set; }
		public string UserLogin { get; set; }
		public string Action { get; set; }
	}
}