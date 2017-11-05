<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/Full_binary.pdf/page1-220px-Full_binary.pdf.jpg" align="right" height="64"/>

# Binary Tree Parser
Binary tree parser is a simple project developed for purpose of deserializing binary tree from a text file or a string. Developing this project, next goals were claimed:

- Read binary tree from a file using API: 
```csharp 
public static Tree BuildTree (StreamReader reader);
```
- Required support for format (_# - stays for null_): 
```
root, left, right
left, subnode, #
right, #, subnode
```
- Parsing file, algorythm should use as minimum iterations as possible
- If algorythm cannot parse a file for any reason it should be able to describe the reason

## Tree Class Description

[Tree class](https://github.com/sergey-solomentsev-nemetos/Solo.BinaryTree/blob/master/Solo.BinaryTree.Constructor/Tree.cs) is a main file used across the project and represents a binary tree with links to _Left_, _Right_ and _Parent_ nodes. And of-course there is a data property represented by a .NET `String` class.

### Creating a tree

Tree creation is hidden by the factory method located in the same class, called `Create` and accepting a string data. Calling a factory method will return you a tree creation result. Result can contain successfully created tree if indicator `IsSuccess` returns true or an error message if indicator `IsFailure` was set. Let's see this in example:

```csharp
var createTreeResult = Tree.Create("data");

if (createTreeResult.IsFailure)
{
    Console.WriteLine("Creating a tree an error occured: {0}.", createTreeResult.FailureMessage);
}

Console.WriteLine("A tree was successfully created with data: {0}.", createTreeResult.Result.Data)
```

### Specifying children

Tree class itself has two methods for children maintenance. A method `OverrideNode` allows to override any of already existing nodes by using reference to another tree and `AddNode` allows to create a new node from String data and add it to the current tree. Let's see an example:

```csharp
public void MyLogic(Tree root, Tree randomNode)
{

    // Override existing node in tree. May produce an error if randomNode already has a parent.
    var overrideNodeResult = root.OverrideNode(randomNode, BinaryChildrenEnum.Left);
    
    // Add node and override it instead of right node. Could return an error if creation of new node were not passed.
    var addNodeResult = root.AddNode("rightNode", BinaryChildrenEnum.Right);
    
    // Indicate a full success by providing result to console.
    if (overrideNodeResult.IsSuccess && addNodeResult.IsSuccess)
    {
        Console.WriteLine("Tree populated with nodes successfully.");
    }
}
```

## API Description

API has several methods dedicated to build a tree from string representation:

- The simpliest way to try things out is to copy the string above and pass it to `BuildTreeByStringInput` overload, like this:

```csharp
var input = 
@"root, left, right
left, subnode, #
right, #, subnode";

Tree result = Api.BuildTreeByStringInput(input);
```

- The same tree you can create by putting an input into a text file and calling a `BuildTreeByFilePath` overload like this:

```csharp
var path = "C:\\data.txt";
Tree result = Api.BuildTreeByFilePath(path);
```

- **Pro-mode** is when you care about streams yourself, for files we have `BuildTree(StreamReader streamReader)` overload, use it like this:

```csharp
var path = "C:\\data.txt";
using (StreamReader streamReader = new StreamReader(path))
{
    var tree = Api.BuildTree(streamReader);
}
```

- **Pro-mode** is also when you know that `StreamReader` is inherited from `TextReader` as other classes like `StringReader` and for this purpose we have `BuildTree(TextReader textReader)` overload, don't hesitate to use it like this:

```csharp
var input = 
@"root, left, right
left, subnode, #
right, #, subnode";

using (TextReader streamReader = new StringReader(input))
{
    var tree = Api.BuildTree(streamReader);
}
```

## Test Results

According to the tests, which you can find in [Performance Tests File](https://github.com/sergey-solomentsev-nemetos/Solo.BinaryTree/blob/master/Solo.BinaryTree.Constructor.Tests/PerformanceTests.cs), an algorythm parsing tree has next results:

- Parsing from memory `200.000+` nodes takes `~3 seconds`, acceptance criteria is `6 seconds`
- Parsing from file on the HDD `200.000+` nodes takes `~11 seconds`, acceptance criteria is `13 seconds`


## Known Issues

### Out of memory exception

When you try to deserialize a tree of 400.000+ nodes program complains that it has no so much memory to use. An issue brought by parser and in most cases is thrown in ["Create Tree From Parsed Values file"](https://github.com/sergey-solomentsev-nemetos/Solo.BinaryTree/blob/master/Solo.BinaryTree.Constructor/Parser/ChainedImplementation/Actions/CreateTreeFromParsedValues.cs).

### Possible cycling references

:scream_cat: OMG, this is not a tree when there are cycles in graph, for more information, please reference [Wikipedia Page](https://en.wikipedia.org/wiki/Tree_(data_structure)#Definition).

The main reason why cyclic references are possible is that there are no check when adding a child node, so possible solutions can be:

- Creating a global validator, so to check the tree when it is created;
- Creating a tree manager which will have all data in one place and will be able to quickly resolve an error;

### Wat? This project is not ideal? :unamused:

Yes, this project is not ideal :stuck_out_tongue_winking_eye: , so you're welcome to contribute. Use these sections to bring your ideas to this project:

- [Issues](https://github.com/sergey-solomentsev-nemetos/Solo.BinaryTree/issues) section of this repository where you can suggest your ideas;
- [Pull Request](https://github.com/sergey-solomentsev-nemetos/Solo.BinaryTree/pulls) where you can suggest your code.