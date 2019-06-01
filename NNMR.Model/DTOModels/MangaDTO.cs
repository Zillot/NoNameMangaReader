using NNMR.Models.DBModels;

namespace NNMR.Model.DTOModels
{
    public class MangaDTO
    {
        public string Name { get; set; }

        public static MangaDTO FromDB(MangaDB manga)
        {
            return new MangaDTO()
            {
                Name = manga.NameENG,
            };
        }
    }
}