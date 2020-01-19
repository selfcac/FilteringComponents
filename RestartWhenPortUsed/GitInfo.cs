﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestartWhenPortUsed
{
    public static class GitInfo
    {
        static string[] GitDependencies = { };

        public static string GetInfo()
        {
            return string.Format("{0} {1}+{2}{3} ({4}) {5}",
                typeof(GitInfo).Namespace,
                ThisAssembly.Git.BaseTag,
                ThisAssembly.Git.Commits,
                ThisAssembly.Git.IsDirty ? "*" : "",
                ThisAssembly.Git.Branch,
                ThisAssembly.Git.Commit
                );
        }

        public static string[] AllGitInfo()
        {
            List<string> dependencies = GitDependencies.Select(x => "+-- " + x).ToList();
            dependencies.Insert(0, GetInfo());
            return dependencies.ToArray();
        }
    }
}
