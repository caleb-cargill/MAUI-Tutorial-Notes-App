using System;
using System.Collections.ObjectModel;

namespace Notes.Models;

internal class AllNotes
{

    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    public AllNotes()
        => LoadNotes();

    public void LoadNotes()
    {
        Notes.Clear();

        string appDirectory = FileSystem.AppDataDirectory;

        IEnumerable<Note> notes =
            Directory
            .EnumerateFiles(appDirectory, "*.notes.txt")
            .Select(fileName =>
                new Note()
                {
                    FileName = fileName,
                    Text = File.ReadAllText(fileName),
                    Date = File.GetCreationTime(fileName)
                })
            .OrderBy(note => note.Date);

        foreach (var note in notes)
            this.Notes.Add(note);
    }
}

