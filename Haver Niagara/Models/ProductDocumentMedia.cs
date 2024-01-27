using System.ComponentModel.DataAnnotations;

namespace Haver_Niagara.Models
{
    //Products can have many media (images, urls, drawings..)
    public class ProductDocumentMedia : UploadedFile
    {
        //Added foreign key back to product

        [Display(Name ="Product")]
        public int ProductID { get; set; }

        public Product Product { get; set; }

        //Each media will belong to a single product
        //has its own ID key (really the uploaded file)
    }
}
