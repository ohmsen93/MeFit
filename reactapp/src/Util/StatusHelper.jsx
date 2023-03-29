export const getStatus = id => {
    switch(id) {
        case 1:
            return "Completed"
        case 2:
            return "Pending"
        case 3:
            return "Failed"
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
        case "Failed":
            return 3
        default:
            return null
    }
}