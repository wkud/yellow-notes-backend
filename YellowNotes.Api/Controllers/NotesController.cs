﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YellowNotes.Core.Dtos;
using YellowNotes.Core.Services;
using System.Threading;
using System.Collections.Generic;
using YellowNotes.Api.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace YellowNotes.Api.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService noteService;

        public NotesController(INoteService noteService) => this.noteService = noteService;

        [HttpGet("{noteId}")]
        [ProducesResponseType(typeof(NoteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NoteDto>> GetNote(int noteId,
            CancellationToken cancellationToken = default)
        {
            var userEmail = HttpContext.GetEmailFromClaims();
            var result = await noteService.GetNote(noteId, userEmail, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }
            else if (result is string)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Tuple<int, IEnumerable<NoteDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetNotes(
            [FromQuery] int takeCount = 20, [FromQuery] int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            if (takeCount < 1 || skipCount < 0)
            {
                return BadRequest();
            }

            var userEmail = HttpContext.GetEmailFromClaims();
            var notes = await noteService.GetNotes(takeCount, skipCount, userEmail,
                cancellationToken);

            return Ok(new { count = notes.Item1, notes = notes.Item2 });
        }

        [HttpPost]
        [ProducesResponseType(typeof(NoteDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateNote([FromBody] NoteDto noteDto,
            CancellationToken cancellationToken = default)
        {
            if (noteDto.NoteId != 0)
            {
                return BadRequest();
            }

            var userEmail = HttpContext.GetEmailFromClaims();
            var note = await noteService.CreateNote(noteDto, userEmail, cancellationToken);

            if (note == null)
            {
                return UnprocessableEntity("Failed to create note");
            }
            return CreatedAtAction(nameof(GetNote), new { noteId = note.NoteId }, note);
        }

        [HttpPut("{noteId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateNote(int noteId, [FromBody] NoteDto noteDto,
            CancellationToken cancellationToken = default)
        {
            noteDto.NoteId = noteId;
            var userEmail = HttpContext.GetEmailFromClaims();
            var result = await noteService.UpdateNote(noteDto, userEmail, cancellationToken);

            if (result is string)
            {
                return Unauthorized(result);
            }
            else
            {
                return (bool)result 
                    ? (IActionResult)NoContent() 
                    : UnprocessableEntity("Failed to update note");
            }
        }

        [HttpDelete("{noteId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNote(int noteId,
            CancellationToken cancellationToken = default)
        {
            var userEmail = HttpContext.GetEmailFromClaims();
            var result = await noteService.DeleteNote(noteId, userEmail, cancellationToken);

            if (result is string)
            {
                return Unauthorized(result);
            }
            else
            {
                return (bool)result ? (IActionResult)NoContent() : NotFound();
            }
        }
    }
}
