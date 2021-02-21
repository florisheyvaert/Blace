using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editing
{
    public abstract class BaseEditorFile : IEquatable<BaseEditorFile>, IEqualityComparer<BaseEditorFile>
    {
        private string _originalContent;

        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsChanged { get => _originalContent != Content; }

        public abstract Task<bool> SaveContent();
        public abstract Task<string> LoadContent();

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

        public bool Equals(BaseEditorFile other)
        {
            return Equals(this, other);
        }

        public bool Equals(BaseEditorFile x, BaseEditorFile y)
        {
            return x._originalContent == y._originalContent && x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] BaseEditorFile obj)
        {
            return obj._originalContent.GetHashCode() + obj.Name.GetHashCode();
        }

        public static bool operator ==(BaseEditorFile obj1, BaseEditorFile obj2)
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

        public static bool operator !=(BaseEditorFile obj1, BaseEditorFile obj2)
        {
            return !(obj1 == obj2);
        }
    }
}
