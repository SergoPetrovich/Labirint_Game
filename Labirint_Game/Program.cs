// See https://aka.ms/new-console-template for more information

enum Direction
{
    North,
    South,
    East,
    West
}

public abstract class MapSite
{
    public abstract void Enter();
}

class Room : MapSite
{
    int roomNumber = 0;
    Dictionary<Direction, MapSite> sides;

    public Room(int roomNo)
    {
        this.roomNumber = roomNo;
        sides = new Dictionary<Direction, MapSite>(4);
    }

    public override void Enter()
    {
        Console.WriteLine("Room");
    }
    public MapSite GetSide(Direction direction)
    {
        return sides[direction];
    }
    public void SetSide(Direction direction, MapSite mapSide)
    {
        this.sides.Add(direction, mapSide);
    }

    public int RoomNumber
    {
        get { return roomNumber; }
        set { roomNumber = value; }
    }
}
class Wall : MapSite
{
    public Wall()
    {
    }
    public override void Enter()
    {
        Console.WriteLine("Wall");
    }
}

class Door : MapSite
{
    Room? room1 = null;
    Room? room2 = null;
    bool isOpen;
    public Door(Room room1, Room room2)
    {
        this.room1 = room1;
        this.room2 = room2;
    }
    public override void Enter()
    {
        Console.WriteLine("Door");
    }
    public Room OtherSideFrom(Room room)
    {
        if (room == room1)
            return room2;
        else
            return room1;
    }
}

class Maze
{
    Dictionary<int, Room> rooms = null;

    public Maze()
    {
        this.rooms = new Dictionary<int, Room>();
    }
    public void AddRoom(Room room)
    {
        rooms.Add(room.RoomNumber, room);
    }
    public Room RoomNo(int number)
    {
        return rooms[number];
    }
}

class MazeFactory
{
    public virtual Maze MakeMaze()
    {
        return new Maze();
    }
    public virtual Wall MakeWall()
    {
        return new Wall();
    }
    public virtual Room MakeRoom(int number)
    {
        return new Room(number);
    }
    public virtual Door MakeDoor(Room room1, Room room2)
    {
        return new Door(room1, room2);
    }
}
class MazeGame
{
    MazeFactory factory = null;
    public Maze CreateMaze(MazeFactory factory)
    {
        this.factory = factory;
        Maze aMaze = this.factory.MakeMaze();
        Room r1 = this.factory.MakeRoom(1);
        Room r2 = this.factory.MakeRoom(2);
        Door aDoor = this.factory.MakeDoor(r1, r2);
        aMaze.AddRoom(r1);
        aMaze.AddRoom(r2);
        r1.SetSide(Direction.North, this.factory.MakeWall());
        r1.SetSide(Direction.East, aDoor);
        r1.SetSide(Direction.South, this.factory.MakeWall());
        r1.SetSide(Direction.West, this.factory.MakeWall());
        r2.SetSide(Direction.North, this.factory.MakeWall());
        r2.SetSide(Direction.East, this.factory.MakeWall());
        r2.SetSide(Direction.South, this.factory.MakeWall());
        r2.SetSide(Direction.West, aDoor);
        return aMaze;
    }
}

