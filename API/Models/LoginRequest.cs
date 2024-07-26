// Models/LoginRequest.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required]
    public string? Login { get; set; }

    [Required]
    public string? Senha { get; set; }
}
