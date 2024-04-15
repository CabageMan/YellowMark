﻿using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using YellowMark.AppServices.Files.Services;
using YellowMark.Contracts.Files;

namespace YellowMark.Api.Controllers;

/// <summary>
/// File controller.
/// </summary>
[ApiController]
[Route("v1/files")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IValidator<Guid> _guidValidator;

    /// <summary>
    /// Init instance of <see cref="FileController"/>.
    /// </summary>
    /// <param name="fileService">File service.</param>
    /// <param name="guidValidator">Guid validator.</param>
    public FileController(
        IFileService fileService,
        IValidator<Guid> guidValidator)
    {
        _fileService = fileService;
        _guidValidator = guidValidator;
    }

    /// <summary>
    /// Upload file.
    /// </summary>
    /// <param name="file">File from form <see cref="IFormFile"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Uploaded file id <see cref="Guid"/></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
    {
        var bytes = await GetBytesAsync(file, cancellationToken);
        var fileDto = new FileDto
        {
            Name = file.FileName,
            Content = bytes,
            ContentType = file.ContentType
        };
        var result = _fileService.UploadFileAsync(fileDto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Download file by id.
    /// </summary>
    /// <param name="id">File id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Ad.</returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DownloadFileById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _fileService.DownloadFileByIdAsync(id, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        Response.ContentLength = result.Content.Length;

        return File(result.Content, result.ContentType);
    }

    /// <summary>
    /// Get file info by id.
    /// </summary>
    /// <param name="id">File id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>File info string.</returns>
    [HttpGet("{id:Guid}/info")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetFileInfoById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _fileService.GetFileInfoByIdAsync(id, cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Delete file by id.
    /// </summary>
    /// <param name="id">File id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Task.</returns>
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteFileById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        // TODO: Handle exception.
        await _fileService.DeleteFileByIdAsync(id, cancellationToken);
        return NoContent();
    }

    // Helpers:
    private static async Task<byte[]> GetBytesAsync(IFormFile formFile, CancellationToken cancellationToken)
    {
        using var ms = new MemoryStream();
        await formFile.CopyToAsync(ms, cancellationToken);
        return ms.ToArray();
    }
}
