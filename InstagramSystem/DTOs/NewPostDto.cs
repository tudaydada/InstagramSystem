using InstagramSystem.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using InstagramSystem.Commons;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace InstagramSystem.DTOs
{
    public class NewPostDto
    {
        public string Content { get; set; }

        public string? Type { get; set; } = EPostType.None.ToString();

        public string? hagtag { get; set; }
    }
}
