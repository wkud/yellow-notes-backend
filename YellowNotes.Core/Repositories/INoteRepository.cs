using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YellowNotes.Core.Dtos;
using YellowNotes.Core.Models;

namespace YellowNotes.Core.Repositories
{
    public interface INoteRepository
    {
        Task<Note> CreateNote(Note note, string email, CancellationToken cancellationToken);

        Task<Note> GetNote(int noteId, string email, CancellationToken cancellationToken);

        Task<IEnumerable<Note>> GetAllNotes(string email, CancellationToken cancellationToken);

        Task<bool> UpdateNote(NoteDto note, string email, CancellationToken cancellationToken);

        Task<bool> DeleteNote(int noteId, string email, CancellationToken cancellationToken);
    }
}
