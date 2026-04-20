using LibraryManagement.Api.Dtos;
using LibraryManagement.Api.Models;
using LibraryManagement.Api.Repositories;

namespace LibraryManagement.Api.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public IEnumerable<MemberResponse> GetMembers()
    {
        return _memberRepository.GetAll()
            .Select(m => new MemberResponse
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber,
                CreatedAt = m.CreatedAt,
                Status = m.Status
            });
    }

    public MemberResponse? GetMemberById(Guid id)
    {
        var member = _memberRepository.GetById(id);
        if (member is null)
            return null;

        return new MemberResponse
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            PhoneNumber = member.PhoneNumber,
            CreatedAt = member.CreatedAt,
            Status = member.Status
        };
    }

    public MemberResponse CreateMember(CreateMemberRequest request)
    {
        var member = new Member
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            Status = "Active"
        };

        var created = _memberRepository.Add(member);

        return new MemberResponse
        {
            Id = created.Id,
            FirstName = created.FirstName,
            LastName = created.LastName,
            Email = created.Email,
            PhoneNumber = created.PhoneNumber,
            CreatedAt = created.CreatedAt,
            Status = created.Status
        };
    }
}
