namespace mywebapp.api.Models
{
    public class Transaction
    {
        public string Id { get; set; }  // MongoDB ObjectId
        public string Title { get; set; }  // Наприклад: "Coffee"
        public decimal Amount { get; set; } // Сума
        public DateTime Date { get; set; }  // Дата транзакції
    }
}
