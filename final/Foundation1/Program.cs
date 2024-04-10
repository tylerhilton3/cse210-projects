using System;
using System.Collections.Generic;

public class Comment {
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text) {
        Name = name;
        Text = text;
    }
}

public class Video {
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds) {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment) {
        Comments.Add(comment);
    }

    public int NumberOfComments() {
        return Comments.Count;
    }
}

public class Program {
    public static void Main() {
        var videos = new List<Video> {
            new Video("Video 1", "Author 1", 300),
            new Video("Video 2", "Author 2", 600),
            new Video("Video 3", "Author 3", 900)
        };

        videos[0].AddComment(new Comment("User 1", "Great video!"));
        videos[0].AddComment(new Comment("User 2", "Very informative."));
        videos[0].AddComment(new Comment("User 3", "Thanks for sharing."));

        videos[1].AddComment(new Comment("User 4", "Interesting content."));
        videos[1].AddComment(new Comment("User 5", "Loved the visuals!"));

        videos[2].AddComment(new Comment("User 6", "Helpful tutorial."));
        videos[2].AddComment(new Comment("User 7", "Nice explanation."));
        videos[2].AddComment(new Comment("User 8", "Good job!"));

        foreach (var video in videos) {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of comments: {video.NumberOfComments()}");

            foreach (var comment in video.Comments) {
                Console.WriteLine($"- {comment.Name} says: \"{comment.Text}\"");
            }

            Console.WriteLine();
        }
    }
}
