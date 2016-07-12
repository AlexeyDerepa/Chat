using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat_002.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Имя пользователя")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string UserName { get; set; }
        // Логин
        [Required]
        [Display(Name = "Логин")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Login { get; set; }
        // Пароль
        [Required]
        [Display(Name = "Пароль")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Password { get; set; }
        //User's image
        public byte[] ImageDate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }



        // Статус
        [Required]
        [Display(Name = "Статус")]
        public int RoleId { get; set; }
        public Role Role { get; set; }




        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
        public User()
        {
            Messages = new List<Message>();
            Topics = new List<Topic>();
        }

    }
}