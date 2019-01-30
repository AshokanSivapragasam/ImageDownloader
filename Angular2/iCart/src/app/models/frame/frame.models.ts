export interface FrameSection {
    id: number;
    headline01: string;
    headline02: string;
    headline03: string;
    frames: Frame[];
}

export interface Frame {
    id: number;
    name: string;
    imageUri: string;
    imageCaption: string;
    framesIn360View: FrameIn360View[];
}

export interface FrameIn360View {
    id: number;
    name: string;
    productCode: string;
    viewOrPerspective: string;
    thumbnailUri: string;
    imageUri: string;
}
