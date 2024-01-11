﻿namespace Contracts.Borrowing
{
    public class Borrowing
    {
        public int Id { get; set; }
        public DateTime BorrowedAt { get; set; }
        public int BorrowedForDays { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public Book.Book Book { get; set; }

        public User.User BorrowedBy { get; set; }
    }
}