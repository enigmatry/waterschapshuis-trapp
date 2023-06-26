const trapStyles = [
  // triangles
  {
    ids: ['a0a0503e-0cd7-0642-73ab-464e7ca0a28e', '586729d8-980e-2a76-81f2-dbb5c57c9d6f'],
    style: { color: 'rgba(224,61,58,0.7)', points: 3, radius: 12, type: 'regular' } // red Conibear
  },
  {
    ids: ['1620509f-4bb2-90ea-637c-af77b636964a'],
    style: { color: 'rgba(190,28,125,0.7)', points: 3, radius: 12, type: 'regular' } // purple Grondklem
  },
  {
    ids: ['eb992687-4000-6956-a688-2fc9242d2e20'],
    style: { color: 'rgba(59,156,20,0.7)', points: 3, radius: 12, type: 'regular' }, // green Lokaasklem
  },
  {
    ids: ['264f0093-6056-110b-1de8-2aefd1d73c4a'],
    style: { color: 'rgba(27,212,223,0.7)', points: 3, radius: 12, type: 'regular' } // teal Postklem
  },
  {
    ids: ['2ff5402a-96b5-6b49-3eed-e5a4372666fb'],
    style: { color: 'rgba(36,122,171,0.7)', points: 3, radius: 12, type: 'regular' } // blue Klemmenrekje
  },
  // stars
  {
    ids: ['c3c795b9-49d2-0722-7f4b-e28bf43da5c4', '13eb51ac-6984-95e9-04ee-ddae1927a499'],
    style: { color: 'rgba(224,61,58,0.7)', points: 5, radius: 12, radius2: 4, angle: 0, type: 'regular' } // red Geweer
  },
  {
    ids: ['935a02f4-69b0-8142-29f8-885124db34bc'],
    style: { color: 'rgba(190,28,125,0.7)', points: 5, radius: 12, radius2: 4, angle: 0, type: 'regular' } // purple Slaan en delven
  },
  {
    ids: ['2fed9c2e-7151-316f-5e5d-644bc5620172', 'ca6c4838-2b63-71c5-3bec-6dbefe7678a2'],
    style: { color: 'rgba(59,156,20,0.7)', points: 5, radius: 12, radius2: 4, angle: 0, type: 'regular' } // green Dood aangetroffen
  },
  // multiple traps on same location
  {
    ids: ['11111111-1111-1111-1111-111111111111'],
    style: { color: 'rgba(9,246,61,0.7)', points: 12, radius: 10, radius2: 8, angle: 0, type: 'regular' } // green
  },
  // empty trap location
  {
    ids: ['00000000-0000-0000-0000-000000000000'],
    style: { color: 'rgba(222,222,222,0.7)', points: 12, radius: 10, radius2: 8, angle: 0, type: 'regular' } // gray
  },
  // squares
  {
    ids: ['6539abe5-081d-a060-31b9-1c5c43f74abb'],
    style: { color: 'rgba(224,61,58,0.7)', points: 4, radius: 10, angle: (Math.PI / 4), type: 'regular' } // red Lokaaskooi
  },
  {
    ids: ['dc90faa1-1ad8-a4f2-22c2-582dcc5d4a84'],
    style: {
      color: 'rgba(190,28,125,0.7)', points: 4, radius: 10, angle: (Math.PI / 4), type: 'regular'
    } // purple Levend vangende kooi
  },
  // circles
  {
    ids: ['e2ba4c87-65fd-2f70-a1ea-68a6a4549db6'],
    style: { color: 'rgba(224,61,58,0.7)', radius: 8, type: 'circle' } // red Schijnduiker
  },
  {
    ids: ['9f91a9d1-77d9-06d9-03a9-18f2efcc0bcc'],
    style: { color: 'rgba(190,28,125,0.7)', radius: 8, type: 'circle' } // purple Duikerafzetting
  },
  {
    ids: ['3c881890-4d00-6b96-25be-67dec7314b2f'],
    style: { color: 'rgba(59,156,20,0.7)', radius: 8, type: 'circle' } // green Slootafzetting met kooi
  }
];

export { trapStyles };
