var input = `L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL`.split("\n").map(x => x.split(""));

function NumberOfAdjecentOccupiedSeats(seats, x, y)
{
    var result = 0;
    var seatsToCheck = [
        { x: -1, y: -1},
        { x: 0, y: -1},
        { x: 1, y: -1},
        { x: -1, y: 0},
        { x: 1, y: 0},
        { x: -1, y: 1},
        { x: 0, y: 1},
        { x: 1, y: 1}
    ];

    for(var i = 0; i < seatsToCheck.length; i++)
    {
        var seatToCheck = seatsToCheck[i];

        if(y + seatToCheck.y < 0 || y + seatToCheck.y >= seats.length || x + seatToCheck.x < 0 || x + seatToCheck.x >= seats[0].length )
            continue;

        if (seats[y + seatToCheck.y][x + seatToCheck.x] == "#")
            result++;
    }

    return result;
}

function DoSwitch(seats)
{
    var newSeats = seats;

    for(var y = 0; y < newSeats.length; y++)
    {
        for(var x = 0; x < newSeats[y].length; x++)
        {
            var seat = newSeats[y][x];

            if (seat == ".")
                continue;
            else if (seat == "L")
                newSeats[y][x] = "#";
            else if (seat == "#" && NumberOfAdjecentOccupiedSeats(seats, x, y) >= 4)
                newSeats[y][x] = "L"
        }
    }

    return {
        seats: newSeats,
        numberOfOccupiedSeats: newSeats.reduce((a,b) => a + b.reduce((c,d) => c + (d == "#" ? 1 : 0), 0), 0)
    };
}

var result = DoSwitch(input);
console.log(NumberOfAdjecentOccupiedSeats(result.seats, 3, 0), result.seats[0][3])
//console.log(DoSwitch(result.seats));