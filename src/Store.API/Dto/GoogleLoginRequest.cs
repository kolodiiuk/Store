namespace Store.API.Dto;

public class GoogleLoginRequest
{
    public string AccessToken { get; set; }
    
    public string Email { get; set; }
    
    public string Name { get; set; }
    
    public string PictureUrl { get; set; }
}
