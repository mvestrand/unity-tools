using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MVest.Unity {

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class CreateUniqueAssetAttribute : ExecuteInEditorAfterAssemblyAttribute {

    private string path;

    public CreateUniqueAssetAttribute(string path) {
        this.path = path;
    }

    public string Path { get { return path; } }

}

}

