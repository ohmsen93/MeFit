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

export const getStatusId = type => {
    switch(type) {
        case "Completed":
            return 1
        case "Pending":
            return 2
        default:
            return null
    }
}