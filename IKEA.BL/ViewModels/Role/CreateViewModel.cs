using Microsoft.AspNetCore.Mvc.Rendering;

public class CreateRoleViewModel
{
    public string RoleName { get; set; }
    public string UserId { get; set; }
    public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
}
