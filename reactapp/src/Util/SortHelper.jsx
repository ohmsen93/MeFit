export const categoryCompare = (a, b) => {
    if (a.categories.length < 1) return 1
    if (b.categories.length < 1) return -1
    if (a.categories[0].name < b.categories[0].name) {
        return -1;
    }
    if (a.categories[0].name > b.categories[0].name) {
        return 1;
    }
    return 0;
}
export const typeCompare = (a, b) => {
    if (a.type < b.type) {
        return -1;
    }
    if (a.type > b.type) {
        return 1;
    }
    return 0;
}
export const musclegroupCompare = (a, b) => {
    if (a.musclegroups.length < 1) return 1
    if (b.musclegroups.length < 1) return -1
    if (a.musclegroups[0].name < b.musclegroups[0].name) {
        return -1;
    }
    if (a.musclegroups[0].name > b.musclegroups[0].name) {
        return 1;
    }
    return 0;
}