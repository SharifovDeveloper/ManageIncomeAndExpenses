using System;

namespace Manage.Models
{
    internal class Expense
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
   
        public Expense()
        {

        }
        public Expense(int categoryid,string description , decimal amount , DateTime date )
        {
            CategoryId = categoryid;
            Description = description;
            Amount = amount;
            Date = date;
     
        }
     
        

        public override string ToString() => $"Id: {Id}, CategoryId: {CategoryId},Description: {Description}, Amount: {Amount}, Date: {Date}, ";


    }
}
