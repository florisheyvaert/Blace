using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editing
{
    public abstract class EditorFile : IEquatable<EditorFile>, IEqualityComparer<EditorFile>
    {
        private string _originalContent;

        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsChanged { get => _originalContent != Content; }

        protected abstract Task<bool> SaveContent();
        protected abstract Task<string> LoadContent();

        public EditorFile(string name)
        {
            Name = name;
        }

        public async Task<string> Load()
        {
            Content = await LoadContent();
            _originalContent = Content;
            return Content;
        }

        public async Task<bool> Save()
        {
            var success = await SaveContent();
            _originalContent = Content;
            return success;
        }

        public bool Equals(EditorFile other)
        {
            return Equals(this, other);
        }

        public bool Equals(EditorFile x, EditorFile y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] EditorFile obj)
        {
            return obj.Name.GetHashCode();
        }

        public static bool operator ==(EditorFile obj1, EditorFile obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }
            if (ReferenceEquals(obj1, null))
            {
                return false;
            }
            if (ReferenceEquals(obj2, null))
            {
                return false;
            }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(EditorFile obj1, EditorFile obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as EditorFile);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
