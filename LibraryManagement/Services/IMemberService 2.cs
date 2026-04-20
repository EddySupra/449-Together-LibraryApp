using LibraryManagement.Api.Dtos;

namespace LibraryManagement.Api.Services;

public interface IMemberService
{
    IEnumerable<MemberResponse> GetMembers();
    MemberResponse? GetMemberById(Guid id);
    MemberResponse CreateMember(CreateMemberRequest request);
}
