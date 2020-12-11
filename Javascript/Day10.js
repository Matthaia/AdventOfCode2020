const input = `71
30
134
33
51
115
122
38
61
103
21
12
44
129
29
89
54
83
96
91
133
102
99
52
144
82
22
68
7
15
93
125
14
92
1
146
67
132
114
59
72
107
34
119
136
60
20
53
8
46
55
26
126
77
65
78
13
108
142
27
75
110
90
35
143
86
116
79
48
113
101
2
123
58
19
76
16
66
135
64
28
9
6
100
124
47
109
23
139
145
5
45
106
41`.split("\n").map(x => parseInt(x));

function Exercise1() 
{
    var adapters = input.sort((a,b) => a - b);

    // Include outlet and device.
    adapters = [0, ...adapters, adapters[adapters.length - 1] + 3]
    
    var diff1 = 0, diff3 = 0;

    for (var i = 0; i < adapters.length - 1; i++)
    {
        var jump = adapters[i + 1] - adapters[i]
        if(jump == 3)
            diff3++;
        else if( jump == 1)
            diff1++
    }

    console.log()

    return {
        adapters,
        differences: {
            1: diff1,
            3: diff3
        }
    };
}

var result1 = Exercise1();
console.log(`Exercise 1: ${result1.differences["1"]} * ${result1.differences["3"]} = ${result1.differences["1"] * result1.differences["3"]}`, result1);

function Exercise2() 
{
    var adapters = input.sort((a,b) => a - b);

    // Include outlet and device.
    adapters = [0, ...adapters, adapters[adapters.length - 1] + 3]

    var diff = []
    for (var i = 0; i < adapters.length - 1; i++)
    {
        var current = adapters[i];
        var nextIndex = i + 1;
        var possibilities = 0;

        while(adapters[nextIndex] <= current + 3)
        {
            possibilities++
            nextIndex++
        }
        diff[i] = possibilities;
    }

    return {
        differences: diff,
        adapters,
        result: diff.reduce((a,b) => a * b, 1)
    }
}

var result2 = Exercise2();
console.log(result2);