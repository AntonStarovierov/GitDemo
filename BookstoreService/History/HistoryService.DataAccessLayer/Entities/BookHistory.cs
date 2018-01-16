namespace HistoryService.DataAccessLayer.Entities
{
	public class BookHistory
	{
		public int Id { get; set; }
		public string UserLogin { get; set; }
		public string Action { get; set; }
		public int BookId { get; set; }
	}
}