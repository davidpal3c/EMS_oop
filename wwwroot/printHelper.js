function printElement(elementId) {
    var printWindow = window.open('', '', 'height=600,width=800');
    var content = document.getElementById(elementId).innerHTML;
    printWindow.document.open();
    printWindow.document.write('<html><head><title>Print</title></head><body>');
    printWindow.document.write(content);
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
}


