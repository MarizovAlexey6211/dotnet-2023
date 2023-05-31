﻿namespace SchoolServer.Dto;

/// <summary>
/// Grade - класс описывающий оценки ученика
/// </summary>
public class GradePostDto
{
    /// <summary>
    /// id Предмета
    /// </summary>
    public int SubjectId { get; set; }

    /// <summary>
    /// id Студента
    /// </summary>
    public int StudentId { get; set; }

    /// <summary>
    /// Оценка
    /// </summary>
    public int Mark { get; set; }

    /// <summary>
    /// Дата оценки
    /// </summary>
    public DateTime Date { get; set; }
}
