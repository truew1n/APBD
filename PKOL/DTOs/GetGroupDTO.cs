namespace PKOL.DTOs
{
    public record GetGroupDTO(int GroupId, string GroupName, List<int> StudentIds);
}