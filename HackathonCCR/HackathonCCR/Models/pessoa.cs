//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HackathonCCR.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class pessoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pessoa()
        {
            this.pessoa_rodovia = new HashSet<pessoa_rodovia>();
            this.pessoa_sente = new HashSet<pessoa_sente>();
        }
    
        public int pessoaId { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public int enderecoId { get; set; }
        public System.DateTime dataNascimento { get; set; }
        public int pontos { get; set; }
        public string contato { get; set; }
    
        public virtual endereco endereco { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pessoa_rodovia> pessoa_rodovia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pessoa_sente> pessoa_sente { get; set; }
    }
}