﻿using Project___ConsoleApp__Library_Management_Application_.Entitys;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

public interface ILoanItemRepository : IGenericRepository<LoanItem>
{
    (int? bookId, int count) MostBorrowedBook();
}
