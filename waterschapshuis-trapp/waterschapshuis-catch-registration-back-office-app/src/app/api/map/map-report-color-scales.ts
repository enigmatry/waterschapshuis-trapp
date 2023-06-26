export { catchNumberColorScale };
export { catchPerKmColorScale };
export { hoursPerKmColorScale };
const alpha = 0.8;
const image = '../../assets/img/Patern.PNG';

const catchNumberColorScale: Fill[] =
[{max: 0, color: `rgba(255,255,255, ${alpha})`},
{ min: 0, max: 5, color: `rgba(205,255,205, ${alpha})`},
{ min: 5, max: 10, color: `rgba(255,255,105, ${alpha})`},
{ min: 10, max: 15, color: `rgba(255,175,75, ${alpha})`},
{ min: 15, max: 20, color: `rgba(255,102,48, ${alpha})`},
{ min: 20, max: 30, color: `rgba(255,15,15, ${alpha})`},
{ min: 30, max: 50, color: `rgba(165,55,0, ${alpha})`},
{ min: 50, max: 75, color: `rgba(135,10,90, ${alpha})`},
{ min: 75, max: 150, color: `rgba(80,0,140, ${alpha})`},
{ min: 150, max: 200, color: `rgba(0,0,0, ${alpha})`},
{ min: 200, image }
];

const catchPerKmColorScale: Fill[] =
[{max: 0.00, color: `rgba(255,255,255, ${alpha})`},
{ min: 0.00, max: 0.01, color: `rgba(205,255,205, ${alpha})`},
{ min: 0.01, max: 0.05, color: `rgba(255,255,105, ${alpha})`},
{ min: 0.05, max: 0.10, color: `rgba(255,175,75, ${alpha})`},
{ min: 0.10, max: 0.15, color: `rgba(255,102,48, ${alpha})`},
{ min: 0.15, max: 0.20, color: `rgba(255,15,15, ${alpha})`},
{ min: 0.20, max: 0.30, color: `rgba(165,55,0, ${alpha})`},
{ min: 0.30, max: 0.50, color: `rgba(135,10,90, ${alpha})`},
{ min: 0.50, max: 0.75, color: `rgba(80,0,140, ${alpha})`},
{ min: 0.75, max: 1.50, color: `rgba(0,0,0, ${alpha})` },
{ min: 1.50, image}
];

const hoursPerKmColorScale: Fill[] =
[{max: 0.00, color: `rgba(255,255,255, ${alpha})`},
{ min: 0.00, max: 0.25, color: `rgba(205,255,205, ${alpha})`},
{ min: 0.25, max: 0.50, color: `rgba(255,255,105, ${alpha})`},
{ min: 0.50, max: 0.75, color: `rgba(255,175,75, ${alpha})`},
{ min: 0.75, max: 1.00, color: `rgba(255,102,48, ${alpha})`},
{ min: 1.00, max: 1.50, color: `rgba(255,15,15, ${alpha})`},
{ min: 1.50, max: 2.00, color: `rgba(165,55,0, ${alpha})`},
{ min: 2.00, max: 3.00, color: `rgba(135,10,90, ${alpha})`},
{ min: 3.00, max: 4.00, color: `rgba(80,0,140, ${alpha})`},
{ min: 4.00, max: 5.00, color: `rgba(0,0,0, ${alpha})` },
{ min: 5.00, image}
];

export class Fill {
    min?: number;
    max?: number;
    color?: string;
    image?: string;
}
