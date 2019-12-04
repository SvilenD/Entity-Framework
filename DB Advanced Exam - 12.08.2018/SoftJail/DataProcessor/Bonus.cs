namespace SoftJail.DataProcessor
{
    using System;
    using System.Linq;

    using Data;
    using static OutputMessages;

    public class Bonus
    {
        public static string ReleasePrisoner(SoftJailDbContext context, int prisonerId)
        {
            var prisoner = context.Prisoners.FirstOrDefault(p => p.Id == prisonerId);
            if (prisoner.ReleaseDate != null)
            {
                prisoner.ReleaseDate = DateTime.Now;
                prisoner.CellId = null;
                context.SaveChanges();
                return String.Format(PrisonerReleasedMessage, prisoner.FullName);
            }
            
            return String.Format(PrisonerSentencedToLifeMessage, prisoner.FullName);
        }
    }
}
