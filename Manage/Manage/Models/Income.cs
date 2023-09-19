using System;

namespace Manage.Models
{
    internal class Income
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Income()
        {

        }
        public Income(int categoryId, string description, decimal amount, DateTime date)
        {
            CategoryId = categoryId;
            Description = description;
            Amount = amount;
            Date = date;
        }

        public override string ToString() => $"Id: {Id}, CategoryId: {CategoryId},Description: {Description}, Amount: {Amount}, Date: {Date}, ";



    }
}
