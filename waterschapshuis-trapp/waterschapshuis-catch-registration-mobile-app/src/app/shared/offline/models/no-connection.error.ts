// https://www.typescriptlang.org/docs/handbook/release-notes/typescript-2-2.html#support-for-newtarget

export class NoConnectionError extends Error {
    constructor(message?: string) {
        super(message);
        Object.setPrototypeOf(this, new.target.prototype);
    }
}
