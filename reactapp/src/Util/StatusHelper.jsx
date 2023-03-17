export const getStatus = id => {
    switch(id) {
        case 1:
            return "Completed"
        case 2:
            return "Pending"
        default:
            return "Error"
    }
}