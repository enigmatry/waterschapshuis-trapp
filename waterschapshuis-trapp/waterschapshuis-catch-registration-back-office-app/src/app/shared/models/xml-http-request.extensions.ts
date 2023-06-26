
export function loadImageWithCustomHeaders(image: HTMLImageElement, url: string, headers: { name: string, value: string }[]): void {
    // load image using custom ajax request
    // https://stackoverflow.com/questions/27000152/set-custom-header-for-the-request-made-from-img-tag/42724593
    const xhr = new XMLHttpRequest();
    xhr.responseType = 'blob';

    xhr.onload = _ => image.src = URL.createObjectURL(xhr.response);
    image.onload = _ => URL.revokeObjectURL(image.src);

    xhr.open('GET', url, true);
    addHeaders(xhr, headers);
    xhr.send();
}

export function addHeaders(xhr: XMLHttpRequest, headers: { name: string, value: string }[]): void {
    headers.forEach(element => {
        xhr.setRequestHeader(element.name, element.value);
    });
}
