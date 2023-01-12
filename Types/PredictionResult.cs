using Types.Attributes;

namespace Types
{
    public class PredictionResult
    {
        #region Properties
        #region Id
        private int id;
        public int ID 
        {
            get
            {
                return id;
            }
            set
            {
                if(id != value)
                {
                    id = value;
                }
            } 
        }
        #endregion Id
        #region Genre
        private string genre;

        [ColumnName("Tahmin Edilen Müzik Türü")]
        public string Genre 
        {
            get
            {
                return genre;
            } 
            set
            {
                if(genre != value)
                {
                    genre = value;
                }
            }
        }
        #endregion Genre
        #region TrackName
        private string name;
        public string Name 
        { 
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            } 
        }
        #endregion TrackName
        #endregion Properties

        #region Constructors
        public PredictionResult(int id, string genre, string name)
        {
            this.id = id;
            this.genre = genre;
            this.name = name;
        }
        #endregion Constructors
    }
}
