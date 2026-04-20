using LibraryManagement.Api.Models;

namespace LibraryManagement.Api.Repositories;

public interface IMemberRepository
{
    IEnumerable<Member> GetAll();
    Member? GetById(Guid id);
    Member Add(Member member);
    bool ExistsByEmail(string email);
}