using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackathonCCR.Models
{
    public class DataPessoa
    {
        public DataPessoa()
        {
            this.pessoasente = new HashSet<pessoa_sente>();
            this.rodovia = new HashSet<rodovia>();
        }

        public int pessoaId { get; set; }

        [Required]
        public string nome { get; set; }
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido.")]
        public string email { get; set; }

        [Required]
        [RegularExpression(@"(?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9])(?=\S*?[^\w\*])\S{8,}$", ErrorMessage = "Senha não corresponde ao pedido.")]
        public string senha { get; set; }

        [Required(ErrorMessage = "O campo Confirmar Senha é obrigatório.")]
        [System.ComponentModel.DataAnnotations.Compare("Senha", ErrorMessage = "Senha não confere!")]
        public string ConfirmarSenha { get; set; }

        public int enderecoId { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        public DateTime dataNascimento { get; set; }
        public int pontos { get; set; }
        [Required]
        public string contato { get; set; }


        //endereço

        [Required]
        public string estado { get; set; }
        [Required]
        public string cidade { get; set; }
        [Required]
        public string endereco { get; set; }
        [Required]
        public string numero { get; set; }
        [Required]
        public string complemento { get; set; }
        [Required]
        public string bairro { get; set; }

        public virtual ICollection<pessoa_sente> pessoasente { get; set; }
        public virtual ICollection<rodovia> rodovia { get; set; }
    }
}